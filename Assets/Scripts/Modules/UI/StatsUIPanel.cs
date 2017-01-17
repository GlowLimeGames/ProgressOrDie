/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class StatsUIPanel : UIElement
	
{
	int AvailablePoint = 50;
	int Body = 0;
	int Mind = 0;
	int Muscle = 0;
	int Spirit = 0;
	int Sp = 0;

	public void BodyPlusClick()
	{
		Body = Body + 1;
		print (Body);
	}
	public void BodyMinusClick()
	{
		Body = Body - 1;
		print (Body);
	}
	public void MindPlusClick()
	{
		Mind = Mind + 1;
		print (Mind);
	}
	public void MindMinusClick()
	{
		Mind = Mind - 1;
		print (Mind);
	}
	public void MusclePlusClick()
	{
		Muscle = Muscle + 1;
		print (Muscle);
	}
	public void MuscleMinusClick()
	{
		Muscle = Muscle - 1;
		print (Muscle);
	}
	public void SpiritPlusClick()
	{
		Spirit = Spirit + 1;
		print ("Spirit+1");
	}
	public void SpiritMinusClick()
	{
		Spirit = Spirit - 1;
		print ("Spirit-1");
	}
	public void SpPlusClick()
	{
		Sp = Sp + 1;
		print ("Sp+1");
	}
	public void SpMinusClick()
	{
		Sp = Sp - 1;
		print ("Sp-1");
	}
}
