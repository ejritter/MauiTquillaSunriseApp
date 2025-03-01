
namespace MauiTquillaSunrise.Model;

public partial class DomainModel : ObservableObject, IComparable<DomainModel>
{
    [ObservableProperty]
    private string domainName;

    public int CompareTo(DomainModel other)
    {
        if (other == null) return 1;
        return string.Compare(this.domainName, other.domainName, StringComparison.Ordinal);
    }
}

