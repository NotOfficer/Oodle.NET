namespace Oodle.NET
{
	public enum OodleLZ_Decode_ThreadPhase
	{
		ThreadPhase1 = 0x1,
		ThreadPhase2 = 0x2,

		Unthreaded = ThreadPhase1 | ThreadPhase2
	}
}