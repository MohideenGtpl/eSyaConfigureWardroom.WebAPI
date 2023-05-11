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
   public class RoomLocationRepository: IRoomLocationRepository
    {
        public async Task<List<DO_RoomLocation>> GetActiveStores()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEcstrm.Where(x=>x.ActiveStatus)
                        .Select(w => new DO_RoomLocation
                        {
                            StoreCode = w.StoreCode,
                            StoreDesc = w.StoreDesc
                        }).OrderBy(o => o.StoreDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_RoomLocation>> GetRoomLocationbyBusinessKey(int businessKey)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                var sr = await db.GtEswrlc
                    .Join(db.GtEcstrm,
                        s => new { s.StoreCode },
                        b => new { b.StoreCode },
                        (s, b) => new { s, b })
                        .Where(w => w.s.BusinessKey == businessKey)
                    .Select(r => new DO_RoomLocation
                    {
                        BusinessKey = r.s.BusinessKey,
                        LocationId = r.s.LocationId,
                        LocationDesc = r.s.LocationDesc,
                        MobileNumber = r.s.MobileNumber,
                        StoreCode = r.s.StoreCode,
                        ActiveStatus = r.s.ActiveStatus,
                        StoreDesc = r.b.StoreDesc
                    })
                    .ToListAsync();

                return sr;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoRoomLocation(DO_RoomLocation obj)
        {
            using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_Exists = db.GtEswrlc.Where(x => x.BusinessKey == obj.BusinessKey && x.LocationId == obj.LocationId).FirstOrDefault();
                        if (is_Exists != null)
                        {
                            return new DO_ReturnParameter { Status = false, Message = "Already Exists" };
                        }
                        int maxval = db.GtEswrlc.Select(d => d.LocationId).DefaultIfEmpty().Max();
                        int loc_Id = maxval + 1;
                        var lo = new GtEswrlc
                        {
                            BusinessKey = obj.BusinessKey,
                            LocationId = loc_Id,
                            LocationDesc = obj.LocationDesc,
                            MobileNumber = obj.MobileNumber,
                            StoreCode = obj.StoreCode,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormId,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };

                        db.GtEswrlc.Add(lo);
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = "Saved Successfully" };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateRoomLocation(DO_RoomLocation obj)
        {
            using (eSyaEnterpriseContext db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {

                    try
                    {
                        var r_loc = db.GtEswrlc.FirstOrDefault(x => x.BusinessKey == obj.BusinessKey && x.LocationId == obj.LocationId);
                        if (r_loc != null)
                        {
                            r_loc.LocationDesc = obj.LocationDesc;
                            r_loc.MobileNumber = obj.MobileNumber;
                            r_loc.StoreCode = obj.StoreCode;
                            r_loc.ActiveStatus = obj.ActiveStatus;
                            r_loc.ModifiedBy = obj.UserID;
                            r_loc.ModifiedOn = DateTime.Now;
                            r_loc.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, Message = "Updated sucessfully." };

                        }
                        else
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Not Exists" };
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> ActiveOrDeActiveRoomLocation(bool status, int businessKey,int locationId)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var r_loc = db.GtEswrlc.FirstOrDefault(x => x.BusinessKey == businessKey && x.LocationId == locationId);
                        if (r_loc == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Not exist" };
                        }

                        r_loc.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, Message = "Activated Successfully." };
                        else
                            return new DO_ReturnParameter() { Status = true, Message = "De Activated Successfully." };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

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
