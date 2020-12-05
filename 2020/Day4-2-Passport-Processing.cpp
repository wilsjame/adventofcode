#include <iostream>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <sstream>
#include <cctype>
using namespace std;

int main() {
    freopen("input.txt", "r", stdin);
    string S, b;

    int ans = 0;
    while (getline(cin, S)) {
        if (S.length() > 0) b += S + " ";

        if (S.length() == 0 || cin.peek() == EOF) {
            bool valid = true;
            string s, k;
            stringstream ss(b);
            unordered_map<string, string> ht = {};

            while (ss >> s) {
                k = s.substr(0, s.find(':'));
                s.erase(0, 4);
                if (k != "cid") ht[k] = s;
            }
            if (ht.size() != 7) valid = false;

            for (auto pr = ht.begin(); pr != ht.end() && valid; pr++) {
                string k = pr->first, v = pr->second;

                if (k == "byr") { 
                    if (stoi(v) < 1920 || stoi(v) > 2002) valid = false;
                }
                if (k == "iyr") {
                    if (stoi(v) < 2010 || stoi(v) > 2020) valid = false;
                }
                if (k == "eyr") { 
                    if (stoi(v) < 2020 || stoi(v) > 2030) valid = false;
                }
                if (k == "hgt") {
                    if (v.back() != 'm' && v.back() != 'n') valid = false;
                    else if (v.back() == 'm') {
                        v.pop_back(); v.pop_back();
                        if (stoi(v) < 150 || stoi(v) > 193) valid = false; 
                    }
                    else if (v.back() == 'n') {
                        v.pop_back(); v.pop_back();
                        if (stoi(v) < 59 || stoi(v) > 76) valid = false;
                    }
                }
                if (k == "hcl") {
                    if (v[0] != '#') valid = false;
                    for (int i = 1; i < 7 && valid; i++) {
                        if (!isdigit(v[i]) && !islower(v[i])) valid = false;
                    }
                }
                if (k == "ecl") {
                    if (v != "amb" && v != "blu" && v != "brn" && v != "gry" && v != "grn" && v != "hzl" && v != "oth") valid = false;
                }
                if (k == "pid") {
                    if (v.size() != 9) valid = false;
                    for (int i = 0; i < 9 && valid ; i++) {
                        if (!isdigit(v[i])) valid = false;
                    }
                }
            }
            ans += valid;
            b = "";
        }
    }
    cout << ans << endl;

    return 0;
}
