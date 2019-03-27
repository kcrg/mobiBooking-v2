import 'package:flutter/material.dart';
import './pages.dart';

class AppDrawer extends StatelessWidget {
  final void Function(Pages) _nawigateToPage;

  AppDrawer(this._nawigateToPage);

  Widget _drawHeader() {
    return DrawerHeader(
      child: Container(
          alignment: Alignment.center,
          margin: EdgeInsets.only(top: 0, left: 0, right: 0),
          child: Image.asset(
            'assets/logo.png',
          )),
      decoration: BoxDecoration(
        color: Colors.white,
      ),
    );
  }

  Widget _drawElement(String text, Pages page, BuildContext context) {
    return Container(
        margin: EdgeInsets.all(10),
        decoration: BoxDecoration(
          color: Theme.of(context).primaryColor,
          boxShadow: <BoxShadow>[
          BoxShadow(
            color: Colors.grey,
            offset: Offset(1, 1),
            blurRadius: 5.0,
          )
        ]),
        child: ListTile(
          title: Text(
            text,
            style: TextStyle(
                color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
          ),
          onTap: () {
            _nawigateToPage(page);
            Navigator.pop(context);
          },
        ));
  }

  Widget _drawItem(int index, BuildContext context) {
    switch (index) {
      case 0:
        return _drawHeader();

      case 1:
        return _drawElement("Dashboard", Pages.dashboard, context);

      case 2:
        return _drawElement("Zarezerwuj salę", Pages.bookRoom, context);

      case 3:
        return _drawElement("Lista sal / rezerwacje", Pages.dashboard, context);

      case 4:
        return _drawElement("Dodaj salę", Pages.addRoom, context);

      case 5:
        return _drawElement("Dodaj użytkownika", Pages.addUser, context);

      case 6:
        return _drawElement("Użytkownicy", Pages.addUser, context);

      default:
        return null;
    }
  }

  @override
  Widget build(BuildContext context) {
    return new Drawer(
      child: ListView.builder(
        padding: EdgeInsets.zero,
        itemCount: 6,
        itemBuilder: (item, index) {
          return _drawItem(index, context);
        },
      ),
    );
  }
}
