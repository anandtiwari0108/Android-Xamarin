package md56e158ba423fb11c9c372b11d945b8bb3;


public class changelayout
	extends android.support.v7.widget.GridLayoutManager.SpanSizeLookup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getSpanSize:(I)I:GetGetSpanSize_IHandler\n" +
			"";
		mono.android.Runtime.register ("Galary.changelayout, Galary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", changelayout.class, __md_methods);
	}


	public changelayout ()
	{
		super ();
		if (getClass () == changelayout.class)
			mono.android.TypeManager.Activate ("Galary.changelayout, Galary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getSpanSize (int p0)
	{
		return n_getSpanSize (p0);
	}

	private native int n_getSpanSize (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
