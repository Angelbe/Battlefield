public interface IUIReservePanelController
{
    public ReserveHandler ReserveToShow { get; set; }
    public void ShowNewReserve(ReserveHandler newReserve);
    public void ClearReserve();
}
