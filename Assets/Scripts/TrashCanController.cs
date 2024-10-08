using UnityEngine;

public class TrashCanController : MonoBehaviour
{
    private static BinlidAnimationController currentBinlidController = null;

    public static void LidController(GameObject obj)
    {
        BinlidAnimationController binlidController = obj.transform.GetComponent<BinlidAnimationController>();
        if (binlidController != null)
        {
            if (currentBinlidController != binlidController)
            {
                if (currentBinlidController != null)
                {
                    currentBinlidController.OpenLidWithState(false);
                }
                currentBinlidController = binlidController;
                currentBinlidController.OpenLidWithState(true);
            }
        }
    }

    public static void CloseCurrentLid()
    {
        if (currentBinlidController != null)
        {
            currentBinlidController.OpenLidWithState(false);
            currentBinlidController = null;
        }
    }
}