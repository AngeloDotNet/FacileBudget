using FluentValidation;
using FacileBudget.Models.InputModels;
using System;

namespace FacileBudget.Models.Validators
{
    public class SpeseCreateValidator : AbstractValidator<SpeseCreateInputModel>
    {
        public SpeseCreateValidator()
        {
            RuleFor(m => m.Descrizione)
                        .NotEmpty().WithMessage("La descrizione è obbligatoria")
                        .MinimumLength(2).WithMessage("La descrizione dev'essere di almeno {MinLength} caratteri");

            RuleFor(m => m.Importo)
                        .NotEmpty().WithMessage("L'importo è obbligatorio");
        }
    }
}