using TodoList.Core.Entities;

namespace TodoList.Application.Constants
{
    public class Messages
    {
        public static string TodoItemNotRemoved = "Todo Silinemedi - Todo Item yok";
        public static string TodoItemRemoved = "Todo Silindi";
        public static string TodoItemNotUpdated = "Todo güncellenemedi";
        public static string TodoItemUpdated = "Todo güncellendi";
        public static string TodoItemAdded = "Todo eklendi";
        public static string TodoItemNotAdded = "Todo eklenemedi";
        public static string ThereIsNoTodoItem = "Todo yok";
        public static string ThereIsTodoItem = "Todo var";
        public static string ThereIsNoTodoItemInList = "Todo listesi boş";
        public static string GetTodoItemList = "Todo listesini al";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string UserBlocked = "Kullanıcı bloklanmış";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
    }
}
