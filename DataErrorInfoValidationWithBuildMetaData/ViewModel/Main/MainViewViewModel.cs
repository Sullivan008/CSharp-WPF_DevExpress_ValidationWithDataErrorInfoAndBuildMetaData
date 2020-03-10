using DataErrorInfoValidationWithBuildMetaData.Dialogs;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace DataErrorInfoValidationWithBuildMetaData.ViewModel.Main
{
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class MainViewViewModel
    {
        #region PROPERTEIS Getters/ Setters

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordConfirm { get; set; }

        public void OnPasswordChanged()
        {
            this.RaisePropertyChanged(model => model.PasswordConfirm);
        }

        public void OnPasswordConfirmChanged()
        {
            this.RaisePropertyChanged(model => model.Password);
        }

        #endregion

        #region VALIDATION RULES

        public static void BuildMetadata(MetadataBuilder<MainViewViewModel> builder)
        {
            builder.Property(x => x.FirstName)
                .Required(() => "Please enter your Last Name!");

            builder.Property(x => x.LastName)
                .Required(() => "Please enter your First Name!");

            builder.Property(x => x.EmailAddress)
                .Required(() => "Please enter your E-mail address")
                .EmailAddressDataType(() => "The email address is not in the correct format!");

            AddPasswordCheck(builder.Property(x => x.Password))
                .Required(() => "Please enter your password!");

            AddPasswordCheck(builder.Property(x => x.PasswordConfirm))
                .Required(() => "Please confirm your password");
        }

        private static PropertyMetadataBuilder<MainViewViewModel, string> AddPasswordCheck(PropertyMetadataBuilder<MainViewViewModel, string> builder)
        {
            const int passwordMinLength = 8;
            const int passwordMaxLength = 20;

            return builder
                .MatchesInstanceRule((name, model) => model.Password == model.PasswordConfirm, () => "Password do not match!")
                .MinLength(passwordMinLength, () => $"Password must be at least {passwordMinLength} characters long!")
                .MaxLength(passwordMaxLength, () => $"Password must be a maximum of {passwordMaxLength} characters long!");
        }

        #endregion

        #region COMMANDS

        public void GetDataInformation()
        {
            string successMessage = "The input data entered were as follows:\n\n" +
                                    "\tFirst name: " + FirstName + "\n" +
                                    "\tLast name: " + LastName + "\n" +
                                    "\tE-mail address: " + EmailAddress + "\n" +
                                    "\tPassword: " + Password + "\n" +
                                    "\tPassword Confirmation: " + PasswordConfirm + "\n";

            new NotificationDialog("Validation Success!", successMessage)
                .ShowMessageBoxByMessageType(MessageBoxImage.Information);
        }

        #endregion
    }
}
