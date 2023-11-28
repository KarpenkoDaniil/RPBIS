function SumbitFuncForSelection(selectedItem)
{
    if (selectedItem != "tables") {
        var form = document.getElementById("RootForm")
        var input = document.getElementById("SecretInput")
        input.value = selectedItem
        console.log(input.value)
        form.submit();
    }
}

function SumbitFuncButton(selectedItem)
{
    var form = document.getElementById("DivMenu")
    var input = document.getElementById("SecretInput")
    var str = "menu_button_" + selectedItem
    input.value = str
    console.log(input.value)
    form.submit();
}

function ChoseFilterInput(i)
{
    var chekbox = document.getElementById(i)
    var element = document.getElementsByName(i+"input")[0]
    if (chekbox.checked)
    {
        element.style.display = "block"
    }
    else
    {
        element.style.display = "none"
    }
}

function ShowOrCloseMenu(button)
{
    var inputs = ["MaterialPageCreate", "MaterialPagePost", "MaterialPageUpdate", "MaterialPageDelete"]
    var someButton = document.getElementsByName("menu_button_"+button)[0]

    var button_1 = document.getElementsByName("menu_button_0")[0]
    var button_2 = document.getElementsByName("menu_button_1")[0]
    var button_3 = document.getElementsByName("menu_button_2")[0]
    var button_4 = document.getElementsByName("menu_button_3")[0]
    
    var form = document.getElementById("DivInputMenu")

    button_1.value = "N"
    button_2.value = "N"
    button_3.value = "N"
    button_4.value = "N"

    var array_1 = [button_1, button_2, button_3, button_4]

    for(i = 0; i < 4; i++)
    {
        if (array_1[i].name == ("menu_button_"+button))
        {
            form.action = inputs[i]
            array_1[i].value = "Y"
            break
        } 
    }
}
