using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;

namespace Proje_Odevi.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public UserController(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<UserModel> users= _databaseContext.Users.ToList().Select(x=>_mapper.Map<UserModel>(x)).ToList();
           

            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
              User user=_mapper.Map<User>(model);
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
