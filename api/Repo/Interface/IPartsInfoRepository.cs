using PartsInfo.Common;
using PartsInfo.Dto;
using X.PagedList;

namespace PartsInfo.Repo.Interface
{
    public interface IPartsInfoRepository
    {
        ResponseBase<IPagedList<PartsInfoDto>> GetAllParts(QueryParam queryParam);
        ResponseBase<PartsInfoDto> GetPartById(int id);
        ResponseBase<string> CreateNewPart(PartsInfoDto dto);
        ResponseBase<string> EditPart(PartsInfoDto dto);
        ResponseBase<string> DeletePart(int id);
        ResponseBase<IEnumerable<dynamic>> GetTreePart(int? id);

    }
}
