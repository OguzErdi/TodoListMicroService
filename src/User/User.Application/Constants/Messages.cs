﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Application.Constants
{
    public class Messages
    {
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string PasswordsDoesntMatch = "Şifreler aynı değil.";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UsernameMustBeAtLeast = "Kullanıcı isminin en az {0} karakter olmalıdır";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string UserFound = "Kullanıcı bulundu";
    }
}
