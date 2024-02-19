namespace BankConsole;

using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public static class Storage{
    static string filepath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user){

        string json = "", usersInFile = "";
        if(File.Exists(filepath))
            usersInFile = File.ReadAllText(filepath);
        
        var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);
        
        if(listUsers == null)
            listUsers = new List<object>();
        
        listUsers.Add(user);

        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

        json = JsonConvert.SerializeObject(listUsers, settings);
        
        File.WriteAllText(filepath, json);
    }

    public static string DeleteUser(int ID){
        string usersInFile = "";
        var listUsers = new List<User>();

        if(File.Exists(filepath))
            usersInFile = File.ReadAllText(filepath);
        
        var listObjects =  JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
            return "No hay usuarios en el archivo";
        
        foreach (object obj in listObjects)
        {
            User newUser;
            JObject user =  (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser =  user.ToObject<Employee>();
            
            listUsers.Add(newUser);
        }

        var userToDelete = listUsers.Where(user=> user.GetID() == ID).Single();
        listUsers.Remove(userToDelete);

        JsonSerializerSettings settings = new JsonSerializerSettings {Formatting = Formatting.Indented};
        string json = JsonConvert.SerializeObject(listUsers, settings);
        
        File.WriteAllText(filepath, json);

        return "Success";
    }

    public static bool IsExistingID(int ID)
        {
            string usersInFile = File.Exists(filepath) ? File.ReadAllText(filepath) : "";
            var listUsers = JsonConvert.DeserializeObject<List<User>>(usersInFile);

            if (listUsers == null)
                return false;

            foreach (User user in listUsers)
            {
                if (user.GetID() == ID)
                {
                    return true;
                }
            }
            return false;
        }
    public static List<User> GetNewUsers(){
        string usersInFile = "";
        var listUsers = new List<User>();
        
        if(File.Exists(filepath))
            usersInFile = File.ReadAllText(filepath);
        
        var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
            return listUsers;
        
        foreach (object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("Tax Regime"))
                newUser = user.ToObject<Client>();
            else
                newUser= user.ToObject<Employee>();
            
            listUsers.Add(newUser);
        }

        var newUsersList = listUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUsersList;
    }
}