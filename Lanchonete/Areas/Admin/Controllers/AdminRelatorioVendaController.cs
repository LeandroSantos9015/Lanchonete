﻿using Lanchonete.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRelatorioVendaController : Controller
    {
        private readonly RelatorioVendasService _relatorioVendasService;

        public AdminRelatorioVendaController(RelatorioVendasService relatorioVendasService)
        {
            _relatorioVendasService = relatorioVendasService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
                minDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!maxDate.HasValue)
                maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioVendasService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }
    }
}