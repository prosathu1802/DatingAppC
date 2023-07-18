using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext context;

        public PhotoRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<Photo> GetPhotoById(int id)
        {
            return await context.Photos
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(photo => photo.Id == id);
        }

        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
        {
            return await context.Photos
                .IgnoreQueryFilters()
                .Where(photo => photo.IsApproved == false)
                .Select(unapprovedPhoto => new PhotoForApprovalDto{
                    Id = unapprovedPhoto.Id,
                    Username = unapprovedPhoto.AppUser.UserName,
                    Url = unapprovedPhoto.Url,
                    IsApproved = unapprovedPhoto.IsApproved
                })
                .ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
            context.Photos.Remove(photo);
        }
    }
}