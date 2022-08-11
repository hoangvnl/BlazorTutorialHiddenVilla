using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext appDbContext, IMapper mapper)
        {
            _db = appDbContext;
            _mapper = mapper;
        }

        public async Task<HotelRoomDto> CreateHotelRoomAsync(HotelRoomDto hotelRoomDto)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();

            return _mapper.Map<HotelRoom, HotelRoomDto>(addedHotelRoom.Entity);
        }

        public async Task<bool> DeleteHotelRoomByIdAsync(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId);

            if (roomDetails != null)
            {
                _db.HotelRooms.Remove(roomDetails);
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<HotelRoomDto>> GetAllHotelRoomsAsync()
        {
            try
            {
                IEnumerable<HotelRoomDto> hotelRoomDtos = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(_db.HotelRooms);

                return hotelRoomDtos;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDto> GetHotelRoomByIdAsync(int roomId)
        {
            try
            {
                HotelRoom foundHotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId);
                HotelRoomDto hotelRoomDto = _mapper.Map<HotelRoom, HotelRoomDto>(foundHotelRoom);

                return hotelRoomDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDto> IsRoomUnique(string name)
        {
            try
            {
                HotelRoom foundHotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name == name);
                HotelRoomDto hotelRoomDto = _mapper.Map<HotelRoom, HotelRoomDto>(foundHotelRoom);

                return hotelRoomDto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<HotelRoomDto> UpdateHotelRoomAsync(int roomId, HotelRoomDto hotelRoomDto)
        {
            try
            {
                if (roomId == hotelRoomDto.Id)
                {
                    var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    var room = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto, roomDetails);

                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;

                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();

                    return _mapper.Map<HotelRoom, HotelRoomDto>(updatedRoom.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
