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
   public class RoomMasterRepository:IRoomMasterRepository
    {
        public async Task<List<DO_RoomMaster>> GetAllRooms()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrrm
                        .Select(w => new DO_RoomMaster
                        {
                            RoomId = w.RoomId,
                            Gender = w.Gender,
                            RoomShortDesc = w.RoomShortDesc,
                            RoomDesc=w.RoomDesc,
                            ActiveStatus = w.ActiveStatus
                        }).OrderBy(o => o.RoomDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoRoomMaster(DO_RoomMaster obj)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_Exists = db.GtEswrrm.Where(d => d.RoomId == obj.RoomId).FirstOrDefault();
                        if (is_Exists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Already Exists" };
                        }
                        int maxval = db.GtEswrrm.Select(a => a.RoomId).DefaultIfEmpty().Max();
                        int _rmId = maxval + 1;
                        var rm = new GtEswrrm
                        {
                            RoomId = _rmId,
                            Gender = obj.Gender,
                            RoomShortDesc = obj.RoomShortDesc,
                            RoomDesc=obj.RoomDesc,
                            FormId = obj.FormId,
                            ActiveStatus = obj.ActiveStatus,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtEswrrm.Add(rm);
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, Message = "Room created Successfully." };
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

        public async Task<DO_ReturnParameter> UpdateRoomMaster(DO_RoomMaster obj)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtEswrrm rm = db.GtEswrrm.Where(d => d.RoomId == obj.RoomId).FirstOrDefault();
                        if (rm == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Not Exists" };
                        }
                        rm.Gender = obj.Gender;
                        rm.RoomShortDesc = obj.RoomShortDesc;
                        rm.RoomDesc = obj.RoomDesc;
                        rm.ModifiedBy = obj.UserID;
                        rm.ModifiedOn = System.DateTime.Now;
                        rm.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, Message = "Room Updated Successfully." };
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

        public async Task<DO_ReturnParameter> ActiveOrDeActiveRoomMaster(bool status, int roomId)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEswrrm _rm = db.GtEswrrm.Where(p => p.RoomId == roomId).FirstOrDefault();
                        if (_rm == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Room  is not exist" };
                        }

                        _rm.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, Message = "Room  Activated Successfully." };
                        else
                            return new DO_ReturnParameter() { Status = true, Message = "Room  De Activated Successfully." };
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
