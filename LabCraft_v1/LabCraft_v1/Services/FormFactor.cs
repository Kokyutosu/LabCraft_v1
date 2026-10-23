using LabCraft_v1.Shared.Services;

namespace LabCraft_v1.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return DeviceInfo.Idiom.ToString();
    }

    public string GetPlatform()
    {
        return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;
    }
}
