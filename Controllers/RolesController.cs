using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_ClubDeportes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Proyecto_ClubDeportes.Views.Roles.ViewModels;

namespace clase11.Controllers;

[Authorize(Roles = "Secretaria, Administrador")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(
        RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        //listar todos los roles
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RoleCreateViewModel model)
    {
        if(string.IsNullOrEmpty(model.RoleName))
        {
            return View();
        }

        var role = new IdentityRole(model.RoleName);
        _roleManager.CreateAsync(role);

        return RedirectToAction("Index");
    }
}