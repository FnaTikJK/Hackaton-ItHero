using API.Modules.ProfilesModule.Entity;

namespace API.Infrastructure.Extensions;

public static class ProfileExtensions
{
  public static string FullName(this ProfileEntity profile)
  {
    var fullName = $"{profile.SecondName} {profile.FirstName}";
    if (profile.ThirdName != null)
      fullName += $" {profile.ThirdName}";

    return fullName;
  }
}
