namespace AuthIdentityWithJwtBearer.Config
{
  public class Settings
  {
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string TokenLifeTimeMinutes { get; set; }
  }
  //"fedaf7d8863b48e197b9287d492b708e"
}