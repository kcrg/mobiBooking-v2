import 'package:flutter/material.dart';
import './app_drawer.dart';
import './login_page.dart';
import './Dashboard/dashboard.dart';
import './pages.dart';
import './add_room.dart';
import './add_user.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  final String appTitle = 'Mobi reservation system';
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: appTitle,
        home: MyHomePage(title: appTitle),
        theme: ThemeData(
            brightness: Brightness.light,
            primaryColor: Color(0xff00b7ce),
            accentColor: Color(0xff902fdb),
            fontFamily: 'Montserrat',
            appBarTheme:
                AppBarTheme(iconTheme: IconThemeData(color: Colors.white)),
            primaryTextTheme:
                TextTheme(title: TextStyle(color: Colors.white))));
  }
}

class MyHomePage extends StatefulWidget {
  final String title;

  MyHomePage({this.title});

  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  Widget selectedPage;
  Widget appDrawer;

  _nawigateToPage(Pages page) {
    switch (page) {
      case Pages.dashboard:
        setState(() {
          selectedPage = new Dashboard();
        });
        break;

      case Pages.addUser:
        setState(() {
          selectedPage = new AddUser();
        });
        break;

      case Pages.addRoom:
        setState(() {
          selectedPage = new AddRoom();
        });
        break;

      default:
    }
  }

  @override
  void initState() {
    super.initState();
    selectedPage = new LoginPage(_login);
    appDrawer = null;
  }

  _login(String token) {
    print(token);
    setState(() {
      appDrawer = new AppDrawer(_nawigateToPage);
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
      drawer: appDrawer,
    );
  }
}