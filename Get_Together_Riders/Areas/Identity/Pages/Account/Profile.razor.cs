using Get_Together_Riders.Data;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

using Microsoft.AspNetCore.Components.Forms;

namespace Get_Together_Riders.Areas.Identity.Pages.Account
{
    public partial class Profile
    {
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationStateTask { get; set; }
        List<ClsUser> _listOfUsers = new List<ClsUser>();
        ClsUser _user = new ClsUser();
        IReadOnlyList<IBrowserFile>? selectedFiles;
        List<string> ImageUrls = new List<string>();
        public string Message { get; set; }
        protected override async void OnInitialized()
        {
            var user = (await authenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                var curruser = await _userManager.GetUserAsync(user);
                var curruserid = curruser.Id;
                GetUserData(curruserid);
            }
            else
            {
                _user = new ClsUser();
            }
        }
        public void GetUserData(string userId)
        {
            ClsUser _user_ = new ClsUser();
            _user = _user_.GetUserByID(userId);
            _user.UserID = _user.UserID;
            _user.Email = _user.Email;
            _user.Password = _user.Password;
            _user.LoginUser_ID = _user.LoginUser_ID;
        }
        public async Task SaveUserAsync()
        {
            Stream stream = selectedFiles.FirstOrDefault().OpenReadStream();
            MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            _user.FileName = selectedFiles.FirstOrDefault().Name;
            _user.FileContent = ms.ToArray();
            stream.Close();
            _user.ImageUrl = @"Images/" + _user.FileName + ".png";
            var path = $"{env.WebRootPath}\\Images\\{_user.FileName + ".png"}";
            var fs = System.IO.File.Create(path);
            fs.Write(_user.FileContent, 0, _user.FileContent.Length);
            fs.Close();
            string uploadsFolder = Path.Combine(env.WebRootPath, "Images");
            if (_user.SaveUser(_user) > 0)
            {
                _user = new ClsUser();
            }
        }
        //private async void LoadFiles(InputFileChangeEventArgs e)
        //{
        //    var imagefile = await e.File.RequestImageFileAsync("Images/jpg",maxWidth:1024,maxHeight:1024);
        //    using (FileStream fileStream=new($"C:\\Users\\MQ\\Downloads\\{e.File.Name}",FileMode.Create))
        //    {
        //        await imagefile.OpenReadStream().CopyToAsync(fileStream);
        //        ImageUrl = $"Images/{e.File.Name}";
        //        this.StateHasChanged();
        //    }
        //    _user.ImageUrl = ImageUrl;
        //}
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            _user.FileName = string.Empty;
            foreach (var imageFile in selectedFiles)
            {
                var resizedImage = await imageFile.RequestImageFileAsync("image/jpg", 100, 100);
                var buffer = new byte[resizedImage.Size];
                await resizedImage.OpenReadStream().ReadAsync(buffer);
                var imageData = $"data:image/jpg;base64,{Convert.ToBase64String(buffer)}";
                ImageUrls.Add(imageData);
                _user.FileName = imageData;
            }
            Message = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
        }
    }
}
