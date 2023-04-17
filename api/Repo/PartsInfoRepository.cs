using Microsoft.EntityFrameworkCore;
using PartsInfo.Common;
using PartsInfo.Dto;
using PartsInfo.Models;
using PartsInfo.Repo.Interface;
using Serilog;
using System.Net.WebSockets;
using X.PagedList;

namespace PartsInfo.Repo
{
    public class PartsInfoRepository : IPartsInfoRepository
    {
        private readonly PartsDbContext _db;
        public PartsInfoRepository(PartsDbContext db)
        {
            _db = db;
        }

        public ResponseBase<IPagedList<PartsInfoDto>> GetAllParts(QueryParam queryParam)
        {
            var respon = new ResponseBase<IPagedList<PartsInfoDto>>();
    
            try
            {
                var query =  _db.PartsInfos.AsQueryable();
                if (!string.IsNullOrEmpty(queryParam.search))
                {
                    query = query.Where(m => m.Code.Contains(queryParam.search) || m.Name.Contains(queryParam.search));
                }
                var queryEnd = query.Select(x => new PartsInfoDto()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Description = x.Description,
                    ParentCode = (x.ParentId != null && _db.PartsInfos.FirstOrDefault(m => m.Id == x.ParentId) != null) ? _db.PartsInfos.FirstOrDefault(m => m.Id == x.ParentId).Code : null,
                    ParentName = (x.ParentId != null && _db.PartsInfos.FirstOrDefault(m => m.Id == x.ParentId) != null) ? _db.PartsInfos.FirstOrDefault(m => m.Id == x.ParentId).Name : null,
                    CreateAt = x.CreateAt ?? DateTime.Now,
                    IsDeleted = x.IsDelete ?? false
                });

                respon.Data = queryEnd.OrderByDescending(x => x.CreateAt).ToPagedList(queryParam.page ?? 1, queryParam.pageSize ?? 25);
                respon.Count = respon.Data.TotalItemCount;
            }
            catch (Exception ex)
            {
                respon.Code = ErrorCodeMessage.InternalExeption.Key;
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }

        public ResponseBase<PartsInfoDto> GetPartById(int id)
        {
            var respon = new ResponseBase<PartsInfoDto>();
            try
            {
                var query = _db.PartsInfos.Where(m => m.Id == id);
                respon.Count = query.Count();
                if (query.Count() > 0)
                {
                    var getPart = query.Select(x => new PartsInfoDto()
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        ParentId = x.ParentId,
                        ParentCode = x.ParentCode,
                        Description = x.Description,
                        CreateAt = x.CreateAt        
                    });
                    
                    respon.Data = getPart.FirstOrDefault();
                }
                

            }
            catch (Exception ex)
            {
                respon.Code = ErrorCodeMessage.InternalExeption.Key;
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }
        public ResponseBase<string> CreateNewPart(PartsInfoDto dto)
        {
            var respon = new ResponseBase<string>();
            try
            {
                int? parentId = null;
                if (!string.IsNullOrEmpty(dto.ParentCode))
                {
                    var findParent = _db.PartsInfos.FirstOrDefault(m => m.Code == dto.ParentCode);
                    if (findParent == null)
                    {
                        respon.Code = ErrorCodeMessage.NoObject.Key;
                        respon.Message = "Không tìm thấy ParentCode";
                        return respon;
                    }
                    parentId = findParent.Id;
                }

                var findCode = _db.PartsInfos.Any(m => m.Code == dto.Code);
                if (findCode)
                {
                    respon.Code = ErrorCodeMessage.AddFail.Key;
                    respon.Message = "Mã đơn vị đã tồn tại rồi";
                    return respon;
                }

                

                var newPart = new Models.PartsInfo()
                {
                    Code = dto.Code,
                    Name = dto.Name,
                    ParentId = parentId,
                    ParentCode = dto.ParentCode,
                    Description = dto.Description
                };
                _db.PartsInfos.Add(newPart);
                _db.SaveChanges();
                respon.Data = newPart.Id.ToString();
            }
            catch (Exception ex)
            {
                respon.Code = ErrorCodeMessage.InternalExeption.Key;
                respon.Message = ErrorCodeMessage.InternalExeption.Value;
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }
        public ResponseBase<string> EditPart(PartsInfoDto dto)
        {
            var respon = new ResponseBase<string>();
            try
            {
                int? parentId = null;
                var getPartEdit = _db.PartsInfos.FirstOrDefault(m => m.Id == dto.Id);
                if (getPartEdit == null)
                {
                    respon.Code = ErrorCodeMessage.EditFail.Key;
                    respon.Message = "Không tìm thấy id của bộ phận";
                    return respon;
                }
                if (!string.IsNullOrEmpty(dto.ParentCode))
                {
                    var checkParentCode = _db.PartsInfos.FirstOrDefault(m => m.Code == dto.ParentCode);
                    if (checkParentCode == null) {
                        respon.Code = ErrorCodeMessage.EditFail.Key;
                        respon.Message = "Không tìm thấy id bộ phận cha";
                        return respon;
                    }
                    parentId = checkParentCode.Id;
                }
                var findCode = _db.PartsInfos.Any(p => p.Code == dto.Code && p.Id != dto.Id);
                if(findCode)
                {
                    respon.Code = ErrorCodeMessage.EditFail.Key;
                    respon.Message = "Mã đơn vị đã được sử dụng rồi";
                    return respon;
                }
                getPartEdit.Name = dto.Name;
                getPartEdit.Code = dto.Code;
                getPartEdit.ParentCode = dto.ParentCode;
                getPartEdit.Description = dto.Description;
                getPartEdit.UpdateAt = DateTime.Now;
                getPartEdit.ParentId = parentId;
                getPartEdit.UpdateBy = dto.Username;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                respon.Code = ErrorCodeMessage.EditFail.Key;
                respon.Message = ErrorCodeMessage.EditFail.Value;
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }

        public ResponseBase<string> DeletePart(int id)
        {
            var respon = new ResponseBase<string>();
            try
            {
                var findPartById = _db.PartsInfos.FirstOrDefault(m => m.Id == id);
                if (findPartById == null)
                {
                    respon.Code = ErrorCodeMessage.DeleteFail.Key;
                    respon.Message = "Không tìm thấy id của bộ phận";
                    return respon;
                }
                var listChild = _db.PartsInfos.Where(c => c.ParentId == id).ToList();
                foreach (var child in listChild)
                {
                    child.ParentCode = null;
                }

                _db.PartsInfos.Remove(findPartById);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                respon.Code = ErrorCodeMessage.DeleteFail.Key;
                respon.Message = ErrorCodeMessage.DeleteFail.Value;
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }

        /// <summary>
        /// truyền vào id null hoặc <= 0 thì sẽ lấy tất cả tree đơn vị
        /// truyền vào id > 0 thì sẽ lấy tất cả tree trừ đơn vị con của nó để update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<IEnumerable<dynamic>> GetTreePart(int? id)
        {
            var respon = new ResponseBase<IEnumerable<dynamic>>();
            try
            {
                var listParts = _db.PartsInfos.ToList();
                if (id != null && id > 0) RemoveDescendants(id??0,listParts);


                respon.Data = GetDescendants(null, listParts);
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
            }
            return respon;
        }

        public static IEnumerable<dynamic> GetDescendants(int? parentId, List<Models.PartsInfo> departments)
        {
            var children = departments.Where(x => x.ParentId == parentId);
            foreach (var child in children)
            {
                yield return new
                {
                    title = child.Name,
                    value = child.Code,
                    children = GetDescendants(child.Id, departments)
                };
            }
        }
        public static void RemoveDescendants(int id, List<Models.PartsInfo> departments)
        {
            var children = departments.Where(x => x.ParentId == id);
            foreach (var child in children)
            {
                RemoveDescendants(child.Id, departments);
                departments.Remove(child);
            }
        }
    }
}
