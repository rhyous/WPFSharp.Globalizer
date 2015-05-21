## WPFSharp.Globalizer ##

A tool to simplify localization for WPF.

You should use this project if:
* Want to runtime language switching? 
* Want to be able to change the style at runtime?
* Want to have localization decoupled from build?
* Want to give users or customers the power to add a language themselves if their language is missing?
* Want to localize WPF without having to put UIDs on every single WPF Element?
* Want the style to change with the language? (or even only certain parts of the style)

## How to install ##

1. In a WPF application, install from NuGet.

    ```
    PM> install-package WPFSharp.Globalizer
    ```

    NuGet installs the dll, some sample language files, an example Xaml page.
    
2. During the NuGet install, when prompted to overwirte your App.xaml and App.xaml.cs file, choose No if you have custom code in their. Choose Yes if you don't.

    If you choose yes, great. Done.
    If you choose no, then manually change App.xaml and App.xaml.cs to inherit GlobalizedApplication.

## How to Add a language ##

A language implementation involves two files in the following folder structure:

Path | Description
---- | ----
Globalization | Top level folder
Globalization\<lang> | Folder for the language using the five character language name
Globalization\en-US\<lang>-style.xaml | Style changes associated with a language 
Globalization\en-US\<lang>.xaml | The language resource file

In the example, there are three languages:

Path | Description
---- | ----
Globalization | Top level folder
Globalization\en-US | Five character language name for english
Globalization\en-US\en-US-style.xaml | Style changes associated with a language 
Globalization\en-US\en-US.xaml | The language resource file
Globalization\es-ES | Five character language name for spanish
Globalization\es-ES\es-ES-style.xaml  | 
Globalization\es-ES\es-ES.xaml | 
Globalization\he-IL | Five character language name for Hebrew
Globalization\he-IL\he-IL-style.xaml | Notice that the flowdirection changes as Hebrew is left to right.
Globalization\he-IL\he-IL.xaml  | 

To add an additional language file, follow these steps.

Easy way:

1. Copy one of the existing language folders.
2. Change the folder name to the 5 character ISO name. This means it is the two letter language code followed by a dash then the two letter country code. You can read RFC5646 (http://tools.ietf.org/html/rfc5646) if you want.

Hard way:

1. Add a folder names using the 5 character ISO name as the  to the Globalization folder. 
2. Manually create the <lang>.xaml file.
3. Make the root of the Xaml file GlobalizationResourceDictionary.
4. The Name property must be the 5 character lang, same as the folder.
5. The LinkedStyle attribute is optional. IF you use it, you must be the name of style file minus the .xaml extension.
6. Add a few strings. The x:Key is the localization key. The inner text is what is displayed when the language is selected.

    ```
    <Globalization:GlobalizationResourceDictionary 
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:Globalization="clr-namespace:WPFSharp.Globalizer;assembly=WPFSharp.Globalizer"
        Name="en-US"
        LinkedStyle="en-US-style"
        >
        <sys:String x:Key="MainWindow_Title">WPF Globalization Example (English)</sys:String>
        <sys:String x:Key="Menu_File">_File</sys:String>
        <sys:String x:Key="Menu_File_Exit">_Exit</sys:String>
    </Globalization:GlobalizationResourceDictionary>
    ```
    
7. (Optional) Create a language style file. For this example, create en-us-style.xaml. Whatever you name it, it must be the same name as the LinkedStyle attribute in the <lang>.xaml file.
8. Make the root object StyleResourceDictionary.
9. Set the Name attribute. Notces the name is the same as the filename without the extension.
10. Add some styles. This file shouldn't be too big. It should only hold styles changes specific to a language.
    ```
    <Globalization:StyleResourceDictionary 
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:Globalization="clr-namespace:WPFSharp.Globalizer;assembly=WPFSharp.Globalizer"
        Name="en-US-style"
        >
        <FlowDirection x:Key="FlowDirection_Default">LeftToRight</FlowDirection>
        <FlowDirection x:Key="FlowDirection_Reverse">RightToLeft</FlowDirection>
        <Style TargetType="{x:Type Label}">
            <Setter Property="Foreground" Value="DarkBlue" />
            <Setter Property="Height" Value="Auto" />
            <Setter Property="Width" Value="Auto" />
            <Setter Property="FontSize" Value="{DynamicResource ResourceKey=CommonFontSize}" />
            <Setter Property="Margin" Value="2" />
        </Style>
    </Globalization:StyleResourceDictionary>
    ```

## How to add a style ##

Styles can also be applied at runtime. You can create any number of styles. Styles are not the same as Language styles.

* Language Styles = Has styles changes specific to a language. For example, in Hebrew the FlowDirection changes to right to left.
* Styles = The overall theme of the application. If you have a blue theme, the blue theme should remain in effect even if the language changes.

Style folder structure is simply. There is a single file for a style.

Styles\Default Style.xaml
Styles\Red.xaml
Styles\Blue.xaml

To add an additional style, follow these steps:

1. Create a style file in the Styles folder. For this example, create DefaultStyle.xaml.
2. Make the root object StyleResourceDictionary.
3. Set the Name attribute. Notces the name is the same as the filename without the extension.
4. Add some styles. This file could get massive as unlike the language file, this could hold all styles.
    ```
    <Globalization:StyleResourceDictionary 
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:Globalization="clr-namespace:WPFSharp.Globalizer;assembly=WPFSharp.Globalizer" 
        Name="DefaultStyle"
        >
        <FlowDirection x:Key="FlowDirection_Default">LeftToRight</FlowDirection>
        <FlowDirection x:Key="FlowDirection_Reverse">RightToLeft</FlowDirection>
        <sys:Double x:Key="CommonFontSize">14</sys:Double>
    
        <Style TargetType="{x:Type Label}">
            <Setter Property="Foreground" Value="DarkOrange" />
            <Setter Property="Height" Value="Auto" />
            <Setter Property="Width" Value="Auto" />
            <Setter Property="FontSize" Value="{StaticResource ResourceKey=CommonFontSize}" />
            <Setter Property="Margin" Value="2" />
        </Style>
        <SolidColorBrush x:Key="MainWindowBackground" Color="White" />
    </Globalization:StyleResourceDictionary>
    ```

