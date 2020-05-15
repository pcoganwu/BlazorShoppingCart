using BlazorShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public interface IStatesRepository
    {
        Task<IList<States>> GetAllStates();
        Task<States> GetState(string stateCode);
        Task<States> AddState(States state);
        Task<States> UpdateState(States state);
        Task<States> DeleteState(string stateCode);
    }
}
