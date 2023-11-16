using tugasmod9.Models.Dto;

namespace tugasmod9.Data
{
    public static class UserStore
    {
        public static List<UserDTO> UserList = new List<UserDTO>
        {
             new UserDTO{Id=1, Username="admin", Password="root"},
             new UserDTO{Id=2, Username="lintang", Password="1234"},
             new UserDTO{Id=3, Username="aggy", Password="1234"},
             new UserDTO{Id=2, Username="fitri", Password="1234"},
             new UserDTO{Id=4, Username="donny", Password="1234"}
        };
    }
}
