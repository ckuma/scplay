from gooey import Gooey
from argparse import ArgumentParser
import sc_playtime

@Gooey(default_size=(550, 350))
def main():
    parser = ArgumentParser(description="SC Playtime Calculator")
    parser.add_argument('-p', '--path',
                    default='C:\Program Files\Roberts Space Industries\StarCitizen\LIVE\logbackups',
                    dest='sc_logs',
                    help="Path to logbackups folder (i.e. ..StarCitizen\LIVE\logbackups)")
    args = parser.parse_args()
    sc_playtime.just_do_it(args.sc_logs)

if __name__ == '__main__':
    main()