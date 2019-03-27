import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import './token_model.dart';
import './CustomInputs/email_custom_input.dart';
import 'CustomInputs/pass_custom_input.dart';
import 'package:shared_preferences/shared_preferences.dart';

class LoginPage extends StatefulWidget {
  final void Function(String) _loginCallback;

  LoginPage(this._loginCallback);

  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {

  final emailController = TextEditingController();
  final passwordController = TextEditingController();
  bool waitForResponse = false;
  bool showError = false;

  _saveToken(String token) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString('token', token);
    //int counter = (prefs.getInt('counter') ?? 0) + 1;
  }

  _login() async {
    var map = new Map<String, dynamic>();
    map["email"] = emailController.text;
    map["password"] = passwordController.text;

    setState(() {
      waitForResponse = true;
    });

    await http.post(
      "https://mobibookingwebapi.azurewebsites.net/api/Account/login",
      body: json.encode(map),
      headers: {"Content-Type": "application/json"},
    ).then((dynamic res) {
      setState(() {
        waitForResponse = false;
      });

      if (res.statusCode == 200) {
        TokenModel tokenModel = TokenModel.fromJson(jsonDecode(res.body));
        _saveToken(tokenModel.token);
        widget._loginCallback(tokenModel.token);
      } else {
        setState(() {
          showError = true;
        });
      }
    });
  }

  Widget _buildLoginForm() {
    return Column(children: <Widget>[
      Container(
          margin: EdgeInsets.only(bottom: 20),
          child: EmailCustomInput(
            controller: emailController,
            text: "Podaj email",
          )),
      PassCustomInput(controller: passwordController, text: 'Podaj hasło'),
      Container(
          margin: EdgeInsets.only(top: 20),
          child: Center(
            child: new RaisedButton(
              padding: const EdgeInsets.all(15.0),
              textColor: Colors.white,
              color: Theme.of(context).primaryColor,
              onPressed: _login,
              child: waitForResponse
                  ? SizedBox(
                      child: new CircularProgressIndicator(
                        strokeWidth: 3,
                      ),
                      height: 20.0,
                      width: 20.0,
                    )
                  : new Text(
                      "Zaloguj",
                      style:
                          TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
                    ),
            ),
          )),
      Opacity(
          opacity: showError ? 1.0 : 0.0,
          child: Container(
            margin: EdgeInsets.only(top: 20),
            child: Text("Nieprawidłowy login lub hasło",
                textAlign: TextAlign.center,
                style: TextStyle(
                    color: Colors.red,
                    fontSize: 20,
                    fontWeight: FontWeight.bold)),
          ))
    ]);
  }

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: <Widget>[
        Container(
            alignment: Alignment.topCenter,
            margin: EdgeInsets.only(top: 100, left: 50, right: 50),
            child: Image.asset(
              'assets/logo.png',
            )),
        Container(
            margin: EdgeInsets.only(top: 100, left: 50, right: 50),
            alignment: Alignment.bottomCenter,
            child: _buildLoginForm())
      ],
    );
  }
}
