package md5124b7a767ba3d25748f610fbd26e90ee;


public class viewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Task1.viewHolder, Task1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", viewHolder.class, __md_methods);
	}


	public viewHolder ()
	{
		super ();
		if (getClass () == viewHolder.class)
			mono.android.TypeManager.Activate ("Task1.viewHolder, Task1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
