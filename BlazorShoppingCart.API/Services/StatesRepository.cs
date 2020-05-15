using BlazorShoppingCart.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorShoppingCart.API.Services
{
    public class StatesRepository : IStatesRepository
    {
        private readonly HalloweenDBContext halloweenDBContext;

        public StatesRepository(HalloweenDBContext halloweenDBContext)
        {
            this.halloweenDBContext = halloweenDBContext;
        }

        public async Task<States> AddState(States state)
        {
            var result = await halloweenDBContext.States.AddAsync(state);
            await halloweenDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<States> DeleteState(string stateCode)
        {
            var state = await halloweenDBContext.States.FirstOrDefaultAsync(s => s.StateCode == stateCode);

            if (state != null)
            {
                halloweenDBContext.Remove(state);
                await halloweenDBContext.SaveChangesAsync();
                return state;
            }
            return null;
        }

        public async Task<IList<States>> GetAllStates()
        {
            return await halloweenDBContext.States.ToListAsync();
        }

        public async Task<States> GetState(string stateCode)
        {
            return await halloweenDBContext.States.FirstOrDefaultAsync(s => s.StateCode == stateCode);
        }

        public async Task<States> UpdateState(States state)
        {
            var result = await halloweenDBContext.States.FirstOrDefaultAsync(s => s.StateCode == state.StateCode);

            if (result != null)
            {
                result.StateName = state.StateName;

                return result;
            }
            return null;
        }
    }
}
