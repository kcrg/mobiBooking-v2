import 'package:flutter/material.dart';
import './pages.dart';

class AppDrawer extends StatelessWidget {
  final void Function(Pages) _nawigateToPage;

  AppDrawer(this._nawigateToPage);

  @override
  Widget build(BuildContext context) {
    return new Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: <Widget>[
          DrawerHeader(
            child: Text('Drawer Header'),
            decoration: BoxDecoration(
              color: Color(0xff002132),
            ),
          ),
          Container(
              margin: EdgeInsets.all(10),
              color: Color(0xff002132),
              child: ListTile(
                title: Text(
                  'Leauge of legends',
                  style: TextStyle(
                      color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
                ),
                onTap: () {
                  _nawigateToPage(Pages.dashboard);
                  Navigator.pop(context);
                },
              )),
          Container(
              margin: EdgeInsets.all(10),
              color: Color(0xff002132),
              child: ListTile(
                title: Text(
                  'Surrender@20',
                  style: TextStyle(
                      color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
                ),
                onTap: () {
                  _nawigateToPage(Pages.addRoom);
                  Navigator.pop(context);
                },
              ))
        ],
      ),
    );
  }
}
