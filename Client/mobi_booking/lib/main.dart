import 'package:flutter/material.dart';
import './dashboard.dart';
import './login_page.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  final String appTitle = 'Mobi reservation system';
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: appTitle,
      home: MyHomePage(title: appTitle),
      color: Colors.blue,
    );
  }
}

class MyHomePage extends StatefulWidget {
  final String title;

  MyHomePage({this.title});

  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  Widget selectedPage;

  @override
  void initState() {
    super.initState();
    selectedPage = new LoginPage(_login);
  }

  _login(String token){
    print(token);
    setState(() {
      selectedPage = new Dashboard();
    });
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Infinite ListView"),
        ),
        body: selectedPage,
        );
  }
}
