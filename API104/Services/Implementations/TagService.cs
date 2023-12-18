using API104.DTOs.Tag;
using Microsoft.EntityFrameworkCore;

namespace API104.Services.Implementations
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateTagDto createTagDto)
        {
            Tag tag = new()
            {
                Name = createTagDto.Name,
            };
            await _repository.AddAsync(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            var tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<GetTagDto> result = new List<GetTagDto>();

            foreach (Tag tag in tags)
            {
                result.Add(new()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                });
            }
            return result;
        }

        public async Task<GetTagDto> GetAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            return new()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }

        public async Task UpdateAsync(int id, UpdateTagDto updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            tag.Name = updateTagDto.Name;

            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }
    }
}
