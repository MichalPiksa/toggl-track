using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogglTrack.API.Abstractions
{
    public record CreateUserRequest(string FirstName, string LastName, string PhotoUrl);
}
