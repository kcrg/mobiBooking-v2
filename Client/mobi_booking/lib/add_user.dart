import 'package:flutter/material.dart';
import './CustomInputs/email_custom_input.dart';
import 'CustomInputs/pass_custom_input.dart';
import './CustomInputs/custom_input.dart';

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
  final userType = TextEditingController();

  _addUser() {}

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Container(
            margin: EdgeInsets.only(left: 30, right: 30, top: 20, bottom: 10),
            child: EmailCustomInput(
              controller: username,
              text: "Podaj email",
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
            margin: EdgeInsets.only(left: 30, right: 30, top: 10, bottom: 10),
            child: CustomInput(
              controller: userType,
              text: 'Uprawnienia',
            )),
        Container(
            margin: EdgeInsets.only(top: 20),
            child: Center(
              child: new RaisedButton(
                padding: const EdgeInsets.all(15.0),
                textColor: Colors.white,
                color: Theme.of(context).primaryColor,
                onPressed: _addUser,
                child: new Text(
                  "Dodaj użytkownika",
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                ),
              ),
            ))
      ],
    );
  }
}
