using ConfigureWardRoom.DL.Entities;
using ConfigureWardRoom.DO;
using ConfigureWardRoom.IF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfigureWardRoom.DL.Repository
{
    public class WardMasterRepository : IWardMasterRepository
    {

        public async Task<List<DO_WardMaster>> GetAllWards()
        {
            try
            {
                using (var db = new eSyaEnterpriseContext())
                {
                    var ds = db.GtEswrms
                        .Select(w => new DO_WardMaster
                        {
                            WardId = w.WardId,
                            WardDesc = w.WardDesc,
                            WardShortDesc=w.WardShortDesc,
                            ActiveStatus=w.ActiveStatus
                        }).OrderBy(o => o.WardDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoWardMaster(DO_WardMaster obj)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_Exists = db.GtEswrms.Where(d => d.WardId == obj.WardId).FirstOrDefault();
                        if (is_Exists != null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Already Exists" };
                        }
                        int maxval = db.GtEswrms.Select(a => a.WardId).DefaultIfEmpty().Max();
                        int _wardId = maxval + 1;
                        var wm = new GtEswrms
                        {
                            WardId = _wardId,
                            WardShortDesc = obj.WardShortDesc,
                            WardDesc = obj.WardDesc,
                            FormId=obj.FormId,
                            ActiveStatus = obj.ActiveStatus,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID
                        };
                        db.GtEswrms.Add(wm);
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, Message = "Ward created Successfully." };
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

        public async Task<DO_ReturnParameter> UpdateWardMaster(DO_WardMaster obj)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        GtEswrms ws = db.GtEswrms.Where(d => d.WardId == obj.WardId).FirstOrDefault();
                        if (ws == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Not Exists" };
                        }
                        ws.WardShortDesc = obj.WardShortDesc;
                        ws.WardDesc = obj.WardDesc;
                        ws.ModifiedBy = obj.UserID;
                        ws.ModifiedOn = System.DateTime.Now;
                        ws.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, Message = "Ward Updated Successfully." };
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

        public async Task<DO_ReturnParameter> ActiveOrDeActiveWardMaster(bool status, int wardId)
        {
            using (var db = new eSyaEnterpriseContext())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEswrms _ward = db.GtEswrms.Where(p => p.WardId == wardId).FirstOrDefault();
                        if (_ward == null)
                        {
                            return new DO_ReturnParameter() { Status = false, Message = "Ward  is not exist" };
                        }

                        _ward.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, Message = "Ward  Activated Successfully." };
                        else
                            return new DO_ReturnParameter() { Status = true, Message = "Ward  De Activated Successfully." };
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

