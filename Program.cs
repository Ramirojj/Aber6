using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger

var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");

string marioFileName = "mario.json";
string dkFileName = "dk.json";
string sf2FileName = "sf2.json";

List<Mario> marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
List<dk> dks = JsonSerializer.Deserialize<List<dk>>(File.ReadAllText(dkFileName))!;
List<sf2> sf2s = JsonSerializer.Deserialize<List<sf2>>(File.ReadAllText(sf2FileName))!;

do
{
  // display choices to user
  Console.WriteLine("1) Display Mario Characters");
  Console.WriteLine("2) Add Mario Character");
  Console.WriteLine("3) Remove Mario Character");
  Console.WriteLine("4) Display Mario Donkey Kong Characters");
  Console.WriteLine("5) Add donkey kong Character");
  Console.WriteLine("6) Remove donkey kong Character");
  Console.WriteLine("7) Display street fighter 2 Characters");
  Console.WriteLine("8) Add street fighter 2 Character");
  Console.WriteLine("9) Remove street fighter 2 Character");
  Console.WriteLine("Enter to quit");
  // input selection
  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);

  /*------------------------------------------------------------------------------------*/
  

switch (choice){
case "1":
    foreach(var c in marios)
    {
      Console.WriteLine(c.Display());
    }
    break;

    /////////////////////////////////////////////
    case "2":  
    Mario mario = new()
    {
      Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
    };
    InputCharacter(mario);          
    marios.Add(mario);  
    File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
    logger.Info($"Character added: {mario.Name}");
    break;
///////////////////////////////////////////////
///
    case "3":
    Console.WriteLine("Enter the Id of the character to remove:");
if (UInt32.TryParse(Console.ReadLine(), out UInt32 marioId))
{
    Mario? character = marios.FirstOrDefault(c => c.Id == marioId);
    if (character == null)
    {
        logger.Error($"Character Id {marioId} not found");
    } else {
        marios.Remove(character);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character Id {marioId} removed");
    }
} else {
    logger.Error($"Invalid Id{marioId} ");
            
}
break;  
/////////////////////////////////////////
case "4":
    foreach(var c in dks)
    {
      Console.WriteLine(c.Display());
    }
    break; 


    ////////////////////////

    case "5":   
    dk dk = new()
    {
      Id = dks.Count == 0 ? 1 : dks.Max(c => c.Id) + 1
    };
    InputCharacter(dk);
    dks.Add(dk);
    File.WriteAllText(dkFileName, JsonSerializer.Serialize(dks));
    logger.Info($"Character added: {dk.Name}");
    break;
    ////////////////////////////
    case "6":
    Console.WriteLine("Enter the Id of the character to remove:");
if (UInt32.TryParse(Console.ReadLine(), out UInt32 dkId)){

    dk? character = dks.FirstOrDefault(c => c.Id == dkId);
    if (character == null)
    {
        logger.Error($"Character Id {dkId} not found");
    } else {
        dks.Remove(character);
        File.WriteAllText(dkFileName, JsonSerializer.Serialize(dks));
        logger.Info($"Character Id {dkId} removed");
    }
} else {
    logger.Error($"Invalid Id{dkId}");
}
break;
//////////////////////////////////////
case "7": 
    foreach(var c in sf2s)
    {
      Console.WriteLine(c.Display());
    } 
    break;
    case "8":
    sf2 sf = new()
    {
      Id = sf2s.Count == 0 ? 1 : sf2s.Max(c => c.Id) + 1
    };
    InputCharacter(sf);
    sf2s.Add(sf);        
    File.WriteAllText(sf2FileName, JsonSerializer.Serialize(sf2s));
    logger.Info($"Character added: {sf.Name}");
    break;  

    case "9":                 
    Console.WriteLine("Enter the Id of the character to remove:");
if (UInt32.TryParse(Console.ReadLine(), out UInt32 sfId)) 
{                 

    sf2? character = sf2s.FirstOrDefault(c => c.Id == sfId);

    if (character == null)
    {
        logger.Error($"Character Id {sfId} not found");
    } else {
        sf2s.Remove(character);
        File.WriteAllText(sf2FileName, JsonSerializer.Serialize(sf2s));
        logger.Info($"Character Id {sfId} removed");
    }

} else {
    logger.Error("Invalid Id{Id}");
}     
break;    
case "":   
logger.Info("Program ended"); 
    break;    
default:    
    logger.Info("Invalid choice");

    break;

}   



}
  while (true);




   

/*-----------------------------------------------------------------------------*/
//logger.Info("Program ended");
static void InputCharacter(Character character)
{
  Type type = character.GetType();
  PropertyInfo[] properties = type.GetProperties();
  var props = properties.Where(p => p.Name != "Id");
  foreach (PropertyInfo prop in props)
  {
    if (prop.PropertyType == typeof(string))
    {
      Console.WriteLine($"Enter {prop.Name}:");
      prop.SetValue(character, Console.ReadLine());
    } else if (prop.PropertyType == typeof(List<string>)) {
      List<string> list = [];
      do {
        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
        string response = Console.ReadLine()!;
        if (string.IsNullOrEmpty(response)){
          break;
        }
        list.Add(response);
      } while (true);
      prop.SetValue(character, list);
    }
  }
}
