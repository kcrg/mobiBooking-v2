import 'package:flutter/material.dart';
import './app_drawer.dart';
import './login_page.dart';
import './Dashboard/dashboard.dart';
import './pages.dart';
import './add_room.dart';
import './add_user.dart';
import './RoomReserv/room_reserv.dart';
import 'package:shared_preferences/shared_preferences.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  final String appTitle = 'Mobi reservation system';
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: appTitle,
        home: MyHomePage(),
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
  MyHomePage();

  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  Widget selectedPage;
  Widget appDrawer;
  bool isLoggedIn = false;
  String title = "";

  _navigateToPage(Pages page) {
    switch (page) {
      case Pages.dashboard:
        setState(() {
          title = "Dashboard";
          selectedPage = new Dashboard(() => _navigateToPage(Pages.bookRoom));
        });
        break;

      case Pages.addUser:
        setState(() {
          title = "Dodaj użytkowanika";
          selectedPage = new AddUser();
        });
        break;

      case Pages.addRoom:
        setState(() {
          title = "Dodaj salę";
          selectedPage = new AddRoom();
        });
        break;

      case Pages.bookRoom:
        setState(() {
          title = "Zarezerwuj salę";
          selectedPage = new RoomReserv();
        });
        break;

      default:
    }
  }

  @override
  void initState() {
    super.initState();

    SharedPreferences.getInstance().then((value) {
      SharedPreferences prefs = value;

      String token = prefs.getString('token');

      if (token != null) {
        _login(token);
      } else {
        setState(() {
          selectedPage = new LoginPage(_login);
          appDrawer = null;
        });
      }
    });
  }

  _login(String token) {
    print(token);
    setState(() {
      appDrawer = new AppDrawer(_navigateToPage);
      title = "Dashboard";
      selectedPage = new Dashboard(() => _navigateToPage(Pages.bookRoom));
      isLoggedIn = true;
    });
  }

  _logout() {
    SharedPreferences.getInstance().then((value) {
      SharedPreferences prefs = value;
      prefs.remove("token");
    });

    setState(() {
      appDrawer = null;
      selectedPage = new LoginPage(_login);
      isLoggedIn = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(title),
        actions: isLoggedIn
            ? <Widget>[
                IconButton(
                  icon: Icon(Icons.exit_to_app),
                  onPressed: _logout,
                )
              ]
            : null,
      ),
      body: selectedPage,
      drawer: appDrawer,
    );
  }
}
