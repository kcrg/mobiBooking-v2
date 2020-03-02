import 'package:flutter/material.dart';
import './CustomInputs/email_custom_input.dart';
import 'CustomInputs/pass_custom_input.dart';
import './CustomInputs/custom_input.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';

class AddUser extends StatefulWidget {
  @override
  _AddUserState createState() => _AddUserState();
}

class _AddUserState extends State<AddUser> {
  final username = TextEditingController();
  final pass = TextEditingController();
  final repeadPass = TextEditingController();
  final name = TextEditingController();
  final sureName = TextEditingController();
  final email = TextEditingController();
  String _currentType;
  List<DropdownMenuItem<String>> _userTypes;

  bool waitForResponse = false;
  bool showSucces = false;
  bool showError = false;

  void initState() { 
    super.initState();
    _getItems();
  }

  _addUser() async {
    setState(() {
      waitForResponse = true;
      showSucces = false;
      showError = false;
    });

    var map = new Map<String, dynamic>();
    map["userName"] = username.text;
    map["password"] = pass.text;
    map["name"] = name.text;
    map["surname"] = sureName.text;
    map["email"] = email.text;
    map["userType"] = _currentType;

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String token = prefs.getString('token');

    await http.post(
      "https://mobibookingwebapi.azurewebsites.net/api/Account/create",
      body: json.encode(map),
      headers: {
        "Content-Type": "application/json",
        "Authorization": "bearer " + token
      },
    ).then((dynamic res) {
      setState(() {
        waitForResponse = false;
      });

      if (res.statusCode == 200) {
        setState(() {
          waitForResponse = false;
          showSucces = true;
          showError = false;
        });
      } else {
        setState(() {
          waitForResponse = false;
          showSucces = false;
          showError = true;
        });
      }
    });
  }

  Widget _createMessage() {
    return Opacity(
        opacity: showSucces || showError ? 1.0 : 0.0,
        child: Container(
          margin: EdgeInsets.only(top: 20),
          child: Text(
              showSucces
                  ? "Użytkownik dodany poprawnie"
                  : "Błąd - taki użytkownik już istnieje",
              textAlign: TextAlign.center,
              style: TextStyle(
                  color: showSucces ? Colors.green : Colors.red,
                  fontSize: 20,
                  fontWeight: FontWeight.bold)),
        ));
  }

   void _getItems() {
    List<DropdownMenuItem<String>> items = new List();
    items.add(new DropdownMenuItem<String>(
        value: "Administrator", child: Text("Administrator")));
    items.add(new DropdownMenuItem<String>(
        value: "User", child: Text("Zwykły użytkownik")));
    setState(() {
      _userTypes = items;
      _currentType = items.first.value;
    });
  }

  void _itemChanged(String value) {
    setState(() {
      _currentType = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 20, bottom: 10),
            child: EmailCustomInput(
              controller: username,
              text: "Nazwa użytkownika",
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: PassCustomInput(
              controller: pass,
              text: 'Hasło',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: PassCustomInput(
              controller: repeadPass,
              text: 'Powtórz hasło',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: name,
              text: 'Imię',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: sureName,
              text: 'Nazwisko',
            )),
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: email,
              text: 'Email',
            )),
        Container(
            margin: EdgeInsets.only(left: 150, right: 160, top: 0, bottom: 0),
            child: new DropdownButton<String>(
              value: _currentType,
              items: _userTypes,
              onChanged: _itemChanged,
            )),
        Container(
          margin: EdgeInsets.only(top: 20),
          child: Center(
            child: new RaisedButton(
              padding: const EdgeInsets.all(15.0),
              textColor: Colors.white,
              color: Theme.of(context).primaryColor,
              onPressed: _addUser,
              child: waitForResponse
                  ? Padding(
                      padding: EdgeInsets.only(left: 60, right: 60),
                      child: SizedBox(
                        child: new CircularProgressIndicator(
                          strokeWidth: 3,
                        ),
                        height: 20.0,
                        width: 20.0,
                      ))
                  : new Text(
                      "Dodaj użytkownika",
                      style:
                          TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                    ),
            ),
          ),
        ),
        _createMessage()
      ],
    );
  }
}
