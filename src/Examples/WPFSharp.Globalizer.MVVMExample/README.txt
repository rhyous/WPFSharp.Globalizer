This MVVM Project is done now. 

To see how to use globalizer with MVVM, look at:

1. PersonViewModel.cs. Notice how the returned text item is a key found in the language xaml file.
2. en-US.xaml (or other language). Notice the Key returned by PersonViewModel properties is there.
3. PersonView.xaml. Notice how the labels call LocalizationBinding instead of just Binding. 
    <Label Content="{globalizer:LocalizationBinding FirstNameLabel, FallbackValue='First Name'}" Name="labelFirstName" Grid.Row="3" FlowDirection="{DynamicResource FlowDirection_Reverse}" Grid.Column="1" />
    <Label Content="{globalizer:LocalizationBinding LastNameLabel, FallbackValue='Last Name'}" Name="labelLastName" Grid.Row="4" FlowDirection="{DynamicResource FlowDirection_Reverse}" Grid.Column="1" />
    <Label Content="{globalizer:LocalizationBinding AgeLabel, FallbackValue=Age, Converter={globconvert:LocalizationConverter}}" Name="labelAge" Grid.Row="5" FlowDirection="{DynamicResource FlowDirection_Reverse}" Grid.Column="1" />
        