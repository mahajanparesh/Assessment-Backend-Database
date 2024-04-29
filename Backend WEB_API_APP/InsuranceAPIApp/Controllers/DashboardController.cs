using InsuranceAPIApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public DashboardController(AngularDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _context.EmployeeDetails.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with UserId {userId} not found.");
            }

            var userPolicies = await _context.UserPolicies.Where(up => up.UserId == userId).ToListAsync();

            var policies = userPolicies.Select(up =>
            {
                var policyDetail = _context.PolicyDetails.FirstOrDefault(pd => pd.PolicyId == up.PolicyId);
                return new
                {
                    PolicyId = policyDetail.PolicyId,
                    PolicyType = policyDetail.PolicyType,
                    Insurer = policyDetail.Insurer,
                    Amount = policyDetail.Amount,
                    StartDate = up.StartDate,
                    EndDate = up.EndDate
                };
            }).ToList();

            return Ok(new
            {
                EmployeeDetail = new
                {
                    user.EmpId,
                    user.Name,
                    user.CompanyName,
                    user.Phone,
                    user.JoinDate
                },
                Policies = policies
            });
        }
    }
}
