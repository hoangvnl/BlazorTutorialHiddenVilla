using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDto> CreateHotelRoomAsync(HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> UpdateHotelRoomAsync(int roomId, HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> GetHotelRoomByIdAsync(int roomId);
        public Task<bool> DeleteHotelRoomByIdAsync(int roomId);
        public Task<IEnumerable<HotelRoomDto>> GetAllHotelRoomsAsync();
        public Task<HotelRoomDto> IsRoomUnique(string name);

    }
}
