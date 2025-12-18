using Ask.Shared.Data.Hotkeys;
using Avalonia.Controls;
using Avalonia.Input;

namespace Ask.DevConsole;

public partial class ConsoleWindow : Window
{
  public ConsoleWindow()
  {
    InitializeComponent();
    KeyDown += OnKeyDown;
  }

  private void OnClosing(object? sender, WindowClosingEventArgs e)
  {
    e.Cancel = true;
    Hide();
  }

  private void OnKeyDown(object? sender, KeyEventArgs e)
  {
   var shouldHide =
      AppHotkeys.DeveloperConsoleHide.Matches(e) ||
      AppHotkeys.DeveloperConsoleToggle.Matches(e);
  
    if (!shouldHide)
      return;
  
    Hide();
    e.Handled = true;
  }
}