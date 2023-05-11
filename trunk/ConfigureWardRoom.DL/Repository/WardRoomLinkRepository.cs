using ConfigureWardRoom.DL.Entities;
using ConfigureWardRoom.DO;
using ConfigureWardRoom.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureWardRoom.DL.Repository
{
   public class WardRoomLinkRepository:IWardRoomLinkRepository
    {
        public async Task<List<DO_RoomLocation>> GetActiveLocations()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrlc.Where(x => x.ActiveStatus)
                        .Select(w => new DO_RoomLocation
                        {
                            LocationId = w.LocationId,
                            LocationDesc = w.LocationDesc
                        }).OrderBy(o => o.StoreDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_WardMaster>> GetActiveWards()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrms.Where(x => x.ActiveStatus)
                        .Select(w => new DO_WardMaster
                        {
                            WardId = w.WardId,
                            WardDesc = w.WardDesc
                        }).OrderBy(o => o.WardDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_RoomMaster>> GetActiveRooms()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrrm.Where(x => x.ActiveStatus)
                        .Select(w => new DO_RoomMaster
                        {
                            RoomId = w.RoomId,
                            RoomDesc = w.RoomDesc
                        }).OrderBy(o => o.RoomDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_WardRoomLink>> GetLoadGridWardRoomLinksbyBusinessKey(int businesskey)
        {
            try
            {
                using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrbl.
                                Join(db.GtEswrrm,
                                wr => new { wr.RoomId },
                                rm => new { rm.RoomId },
                                (wr, rm) => new { wr, rm }).
                                Join(db.GtEswrms,
                                wrw => new { wrw.wr.WardId },
                                wm => new { wm.WardId },
                                (wrw, wm) => new { wrw, wm }).
                                Join(db.GtEswrlc,
                                wrwl => new { wrwl.wrw.wr.LocationId},
                                lm => new { lm.LocationId},
                               (wrwl, lm) => new { wrwl, lm })
                               .Where(x =>x.wrwl.wrw.wr.BusinessKey==businesskey)
                               .Select(r => new DO_WardRoomLink
                               {
                                   BusinessKey =r.wrwl.wrw.wr.BusinessKey,
                                   WardId = r.wrwl.wrw.wr.WardId,
                                   RoomId = r.wrwl.wrw.wr.RoomId,
                                   LocationId = r.wrwl.wrw.wr.LocationId,
                                   NoOfBeds = r.wrwl.wrw.wr.NoOfBeds,
                                   OccupiedBeds = r.wrwl.wrw.wr.OccupiedBeds,
                                   ActiveStatus = r.wrwl.wrw.wr.ActiveStatus,
                                   WardDescDesc=r.wrwl.wm.WardDesc,
                                   RoomDesc=r.wrwl.wrw.rm.RoomDesc,
                                   RoomLocationDesc=r.lm.LocationDesc

                               }).ToListAsync();
                    return await ds;
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_WardRoomLink> GetWardRoomLink(int businesskey,int roomId,int wardId,int locationId)
        {
            try
            {
                using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
                {
                    var result = db.GtEswrbl
                        .Where(i => i.BusinessKey == businesskey && i.RoomId== roomId && i.WardId==wardId && i.LocationId==locationId)
                                 .Select(x => new DO_WardRoomLink
                                 {
                                     BusinessKey = x.BusinessKey,
                                     WardId = x.WardId,
                                     RoomId = x.RoomId,
                                     LocationId=x.LocationId,
                                     NoOfBeds=x.NoOfBeds,
                                     OccupiedBeds=x.OccupiedBeds,
                                     ActiveStatus = x.ActiveStatus,
                                     l_ClassParameter =db.GtEswrpa.
                                     Where(y=>y.BusinessKey==businesskey && y.WardId==wardId && y.RoomId==roomId
                                     ).Select(p => new DO_eSyaParameter
                                     {
                                         ParameterID = p.ParameterId,
                                         ParmPerc = p.ParmPerc,
                                         ParmAction = p.ParmAction,
                                         ParmDesc = p.ParmDesc,
                                         ParmValue = p.ParmValue,
                                         ActiveStatus=p.ActiveStatus
                                     }).ToList()
                                 }
                        ).FirstOrDefaultAsync();

                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> AddOrUpdatetWardRoomLink(DO_WardRoomLink obj)
        {
            using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var wrlink = db.GtEswrbl.Where(x => x.BusinessKey == obj.BusinessKey && x.WardId == obj.WardId
                          && x.RoomId == obj.RoomId && x.LocationId == obj.LocationId).FirstOrDefault();
                        if (wrlink == null)
                        {                            
                                var wr_link = new GtEswrbl
                                {
                                    BusinessKey = obj.BusinessKey,
                                    WardId = obj.WardId,
                                    RoomId = obj.RoomId,
                                    LocationId = obj.LocationId,
                                    NoOfBeds = obj.NoOfBeds,
                                    OccupiedBeds = obj.OccupiedBeds,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId = obj.FormId,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID
                                };
                                db.GtEswrbl.Add(wr_link);

                            foreach (DO_eSyaParameter cp in obj.l_ClassParameter)
                            {
                                var cPar = db.GtEswrpa.Where(x => x.BusinessKey == obj.BusinessKey && x.WardId == obj.WardId && x.RoomId == obj.RoomId && x.ParameterId == cp.ParameterID).FirstOrDefault();
                                if (cPar == null)
                                {
                                    var cParameter = new GtEswrpa
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        WardId = obj.WardId,
                                        RoomId = obj.RoomId,
                                        ParameterId = cp.ParameterID,
                                        ParmPerc = cp.ParmPerc,
                                        ParmAction = cp.ParmAction,
                                        ParmDesc = cp.ParmDesc,
                                        ParmValue = cp.ParmValue,
                                        ActiveStatus = cp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEswrpa.Add(cParameter);
                                }
                                else
                                {
                                    cPar.ParmAction = cp.ParmAction;
                                    cPar.ParmDesc = cp.ParmDesc;
                                    cPar.ParmPerc = cp.ParmPerc;
                                    cPar.ParmValue = cp.ParmValue;
                                    cPar.ActiveStatus = cp.ActiveStatus;
                                    cPar.ModifiedBy = obj.UserID;
                                    cPar.ModifiedOn = System.DateTime.Now;
                                    cPar.ModifiedTerminal = obj.TerminalID;
                                }
                            }

                        }
                        else
                        {
                            wrlink.NoOfBeds = obj.NoOfBeds;
                            wrlink.OccupiedBeds = obj.OccupiedBeds;
                            wrlink.ActiveStatus = obj.ActiveStatus;
                            wrlink.ModifiedBy = obj.UserID;
                            wrlink.ModifiedOn = System.DateTime.Now;
                            wrlink.ModifiedTerminal = obj.TerminalID;

                            foreach (DO_eSyaParameter cp in obj.l_ClassParameter)
                            {
                                var cPar = db.GtEswrpa.Where(x => x.BusinessKey == obj.BusinessKey && x.WardId == obj.WardId && x.RoomId== obj.RoomId && x.ParameterId==cp.ParameterID).FirstOrDefault();
                                if (cPar != null)
                                {
                                    cPar.ParmAction = cp.ParmAction;
                                    cPar.ParmDesc = cp.ParmDesc;
                                    cPar.ParmPerc = cp.ParmPerc;
                                    cPar.ParmValue = cp.ParmValue;
                                    cPar.ActiveStatus = cp.ActiveStatus;
                                    cPar.ModifiedBy = obj.UserID;
                                    cPar.ModifiedOn = System.DateTime.Now;
                                    cPar.ModifiedTerminal = obj.TerminalID;
                                }
                                else
                                {
                                    var cParameter = new GtEswrpa
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        WardId=obj.WardId,
                                        RoomId=obj.RoomId,
                                        ParameterId = cp.ParameterID,
                                        ParmPerc = cp.ParmPerc,
                                        ParmAction = cp.ParmAction,
                                        ParmDesc = cp.ParmDesc,
                                        ParmValue = cp.ParmValue,
                                        ActiveStatus = cp.ActiveStatus,
                                        FormId = obj.FormId,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID,

                                    };
                                    db.GtEswrpa.Add(cParameter);
                                }

                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = "Linked Sucessfully." };


                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> ActiveOrDeActiveWardRoomLink(DO_WardRoomLink obj)
        {
            using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtEswrbl wrlink = db.GtEswrbl.Where(w => w.BusinessKey == obj.BusinessKey && w.WardId==obj.WardId
                        && w.RoomId==obj.RoomId && w.LocationId==obj.LocationId).FirstOrDefault();
                        if (wrlink == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Not exist" };
                        }

                        wrlink.ActiveStatus = obj.status;

                        //var wparam = db.GtEswrpa.Where(w => w.BusinessKey == obj.BusinessKey && w.WardId==obj.WardId
                        //&& w.RoomId==obj.RoomId).ToList();
                        //foreach (var p in wparam)
                        //{
                        //    db.GtEswrpa.Remove(p);
                        //}

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (obj.status == true)
                            return new DO_ReturnParameter() { Status = true, Message = "Activated Successfully." };
                        else
                            return new DO_ReturnParameter() { Status = true, Message = "De Activated Successfully." };
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
