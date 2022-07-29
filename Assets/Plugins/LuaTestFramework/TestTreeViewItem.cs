using UnityEditor.IMGUI.Controls;

namespace LuaTestFramework
{
    public class TestTreeViewItem : TreeViewItem
    {
        private TestState state = TestState.IDLE;
        public TestState State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
