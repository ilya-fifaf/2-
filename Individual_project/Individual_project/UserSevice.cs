namespace Individual_project;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public static class UserService
{
    private static string FilePath => "users.json";

    public static List<Person> GetAllUsers()
    {
        if (!File.Exists(FilePath))
            return new List<Person>();
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<List<Person>>(json);
    }

    public static void SaveAllUsers(List<Person> users)
    {
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static Person GetUser(string login)
    {
        return GetAllUsers().FirstOrDefault(u => u.Login == login);
    }

    public static void AddRequest(string login, UserRequest request)
    {
        var users = GetAllUsers();
        var user = users.FirstOrDefault(u => u.Login == login);
        if (user != null)
        {
            if (user.Requests == null)
                user.Requests = new List<UserRequest>();
            user.Requests.Add(request);
            SaveAllUsers(users);
        }
    }

    public static List<UserRequest> GetRequests(string login)
    {
        var user = GetUser(login);
        return user?.Requests ?? new List<UserRequest>();
    }
}