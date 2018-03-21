package md54d27b6070dac2e36481ee242a98a02af;


public class weekfrag
	extends android.support.v4.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("customcalendarview.weekfrag, customcalendarview, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", weekfrag.class, __md_methods);
	}


	public weekfrag ()
	{
		super ();
		if (getClass () == weekfrag.class)
			mono.android.TypeManager.Activate ("customcalendarview.weekfrag, customcalendarview, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public weekfrag (java.util.Calendar p0)
	{
		super ();
		if (getClass () == weekfrag.class)
			mono.android.TypeManager.Activate ("customcalendarview.weekfrag, customcalendarview, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Java.Util.Calendar, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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
