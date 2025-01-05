// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Ask_Me_Now.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ask_Me_Now.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Utilizator> _userManager;
        private readonly SignInManager<Utilizator> _signInManager;

        public IndexModel(
            UserManager<Utilizator> userManager,
            SignInManager<Utilizator> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name ="Identificare utilizator - prin Email")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// rolurile utilizatorului si altele
        [Display(Name ="Rolul utilizatorului - ca Admin, puteti modifica rolurile din sectiunea Utilizatori.")]
        public string UserRoles { get; set; }

        public string UserId { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

   

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Numar de telefon")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Numele tau")]
            [MinLength(1, ErrorMessage = "Numele nu poate avea mai putin de un caracter!")]
            [MaxLength(25, ErrorMessage = "Numele nu poate avea mai mult de 25 de caractere!")]
            public string Nume { get; set; }

            [Display(Name = "Prenumele tau")]
            [MinLength(1, ErrorMessage = "Prenumele nu poate avea mai putin de un caracter!")]
            [MaxLength(25, ErrorMessage = "Prenumele nu poate avea mai mult de 25 de caractere!")]
            public string Prenume { get; set; }

            [Display(Name = "Descrierea ta")]
            [MaxLength(200, ErrorMessage = "Profilul nu poate sa contina o descriere mai lunga de 200 de caractere!")]
            public string Descriere { get; set; }

        }

        private async Task LoadAsync(Utilizator user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userId = _userManager.GetUserId(User);
            var nume = user.Nume;
            var prenume = user.Prenume;
            var descriere = user.Descriere;

            var roluri = await _userManager.GetRolesAsync(user);
            var roluriString = string.Join(',', roluri);

            Username = userName;
            UserId=userId;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nume = nume,
                Prenume= prenume,
                Descriere = descriere
            };

            UserRoles = roluriString;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nu s-a putut incarca user-ul cu ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        //stergere cont
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Nu s-a putut gasi utilizatorul cu ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                StatusMessage = "Eroare la stergerea contului.";
                return RedirectToPage();
            }

            StatusMessage = "Contul a fost sters cu succes.";
            return RedirectToPage("/Register"); 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nu s-a putut incarca user-ul cu ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //nr tel modificat
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Eroare la modificarea numarului de telefon.";
                    return RedirectToPage();
                }
            }

            //nume modificat
            if (Input.Nume != user.Nume)
            {
                user.Nume = Input.Nume; 
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Eroare la modificarea numelui.";
                    return RedirectToPage();
                }
            }

            //prenume modificat
            if (Input.Prenume != user.Prenume)
            {
                user.Prenume = Input.Prenume;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Eroare la modificarea prenumelui.";
                    return RedirectToPage();
                }
            }

            //descriere modificata
            if (Input.Descriere != user.Descriere)
            {
                user.Descriere = Input.Descriere;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Eroare la modificarea descrierii.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Actiune realizata cu succes!";
            return RedirectToPage();
        }
    }
}
