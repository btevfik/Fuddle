/// <reference name="MicrosoftAjax.js"/>
// Global variables 
var aboutmeLabel, aboutmeTextBox;

Sys.Application.add_init(AppInit);

function AppInit(sender) {
    aboutmeLabel = document.getElementById(pageNameElements.label);
    aboutmeTextBox = document.getElementById(pageNameElements.textbox);

  $addHandler(aboutmeLabel, "click", aboutmeLabel_Click);
   
  $addHandler(aboutmeTextBox, "blur", aboutmeTextBox_Blur);
  $addHandler(aboutmeTextBox, "keydown", aboutmeTextBox_KeyDown);
}

function aboutmeLabel_Click() {
  aboutmeTextBox.value = aboutmeLabel.innerHTML;
   
  aboutmeLabel.style.display = 'none';
  aboutmeTextBox.style.display = '';
  
  aboutmeTextBox.focus();
}

// If the enter key is pressed,
//  then blur the TextBox.
function aboutmeTextBox_KeyDown(event) {
  if (event.keyCode == 13) {
    event.preventDefault();
    aboutmeTextBox.blur();
  }
}

function aboutmeTextBox_Blur() {
  var labelUpdated;
  
  if (aboutmeLabel.textContent == aboutmeTextBox.value)
    labelUpdated = false;
  else 
    labelUpdated = true;
    
  aboutmeLabel.innerHTML = aboutmeTextBox.value;
  
  aboutmeTextBox.style.display = 'none';
  aboutmeLabel.style.display = '';
  
  if (labelUpdated)
    PageMethods.SetAboutMe(aboutmeTextBox.value);
}