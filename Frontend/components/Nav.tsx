"use client";

import { Disclosure } from "@headlessui/react";
import { Bars3Icon, XMarkIcon } from "@heroicons/react/24/outline";
import classNames from "classnames";
import Link from "next/link";
import { usePathname } from "next/navigation";
import Image from "next/image";

import Logo from "../public/images/logo_2.svg";

const Links = [
  { name: "Home", href: "/" },
  { name: "Stats", href: "/stats" },
  { name: "Users", href: "/users" },
  { name: "Calls", href: "/calls" },
];

const Nav = () => {
  const path = usePathname();
  const rootSegment = path.split("/")[1];

  return (
    <Disclosure as="nav" className="bg-slate-900">
      {({ open }) => (
        <>
          <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <div className="flex h-16 items-center justify-between">
              <div className="flex items-center">
                <Image
                  alt="Finity Logo"
                  src={Logo}
                  height={48}
                  width={48}
                  className="mr-6"
                />
                <div className="hidden sm:block">
                  <div className="flex space-x-4">
                    {Links.map((link) => (
                      <Link
                        key={link.href}
                        href={link.href}
                        className={classNames(
                          "rounded-md hover:bg-gray-950 px-3 py-2 text-sm font-medium text-white",
                          rootSegment === link.href.split("/")[1]
                            ? "bg-gray-950"
                            : ""
                        )}
                      >
                        {link.name}
                      </Link>
                    ))}
                  </div>
                </div>
              </div>
              <div className="-mr-2 flex sm:hidden">
                <Disclosure.Button className="relative inline-flex items-center justify-center rounded-md p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white">
                  <span className="absolute -inset-0.5" />
                  <span className="sr-only">Open main menu</span>
                  {open ? (
                    <XMarkIcon className="block h-6 w-6" aria-hidden="true" />
                  ) : (
                    <Bars3Icon className="block h-6 w-6" aria-hidden="true" />
                  )}
                </Disclosure.Button>
              </div>
            </div>
          </div>

          <Disclosure.Panel className="sm:hidden">
            <div className="space-y-1 px-2 pb-3 pt-2">
              {Links.map((link) => (
                <Disclosure.Button
                  key={link.href}
                  as="a"
                  href={link.href}
                  className={classNames(
                    "block rounded-md bg-gray-900 px-3 py-2 text-base font-medium text-white",
                    rootSegment === link.href.split("/")[1] ? "underline" : ""
                  )}
                >
                  {link.name}
                </Disclosure.Button>
              ))}
            </div>
          </Disclosure.Panel>
        </>
      )}
    </Disclosure>
  );
};

export default Nav;
