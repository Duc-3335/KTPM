using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IAnimalAnimalFacilityRepository
    {
        Task<IEnumerable<AnimalAnimalFacility>> GetAll();
        Task<AnimalAnimalFacility> GetIdByAsync(int id);
        Task<IEnumerable<AnimalAnimalFacility>> GetAnimalByFacility(string name);
        Task<IEnumerable<AnimalAnimalFacility>> GetFacilityByAnimal(string name);
        Task<IEnumerable<AnimalAnimalFacility>> GetAnimalByFacility(int id);
        Task<IEnumerable<AnimalAnimalFacility>> GetFacilityByAnimal(int id);
        Task<IEnumerable<AnimalAnimalFacility>> GetAnimalOrFacility(string name);
        bool Add(AnimalAnimalFacility af);
        bool Update(AnimalAnimalFacility af);
        bool Delete(AnimalAnimalFacility af);
        bool Save();

    }
}
