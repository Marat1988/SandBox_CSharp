using MyCompany.Domain.Entities;
using MyCompany.Models;

namespace MyCompany.Infrastructure
{
    public static class HelperDTO
    {
        public static ServiceDTO TransformService(Service entity)
        {
            ServiceDTO entityDTO = new()
            {
                Id = entity.Id,
                CategoryName = entity.ServiceCategory?.Title,
                Title = entity.Title,
                DescriptionShort = entity.DescriptionShort,
                Description = entity.Description,
                PhotoFileName = entity.Photo,
                Type = entity.Type.ToString()
            };
            return entityDTO;
        }

        public static IEnumerable<ServiceDTO> TransformService(IEnumerable<Service> entities)
        {
            List<ServiceDTO> entitiesDTO = new List<ServiceDTO>();
            foreach (Service entity in entities)
            {
                entitiesDTO.Add(TransformService(entity));
            }
            return entitiesDTO;
        }
    }
}
