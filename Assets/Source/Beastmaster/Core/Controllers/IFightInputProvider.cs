using Beastmaster.Core.Primitives;

namespace Beastmaster.Core.Controllers
{
    public interface IFightInputProvider
    {
        Coordinates GetTileUnderCursor();
        int GetUnitIdUnderCursor();
        bool LMBClicked();
        bool RMBClicked();
    }
}