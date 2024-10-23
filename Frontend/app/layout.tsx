import type { Metadata } from "next";
import { Inter } from "next/font/google";
import Providers from "./providers";
import Nav from "@/components/Nav";

import "../styles/globals.css";
import "react-toastify/dist/ReactToastify.css";

import { ToastContainer, ToastContainerProps } from "react-toastify";

const inter = Inter({ subsets: ["latin"] });

const toastConfig: ToastContainerProps = {
  position: "top-right",
  autoClose: 5000,
  hideProgressBar: true,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  theme: "dark",
};

export const metadata: Metadata = {
  title: "Assessment",
  description: "Assessment!",
};

const RootLayout = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  return (
    <html lang="en">
      <head></head>
      <body className={inter.className}>
        <header>
          <Nav />
        </header>
        <div className="mx-auto max-w-7xl">
          <div className="py-10">
            <div className="px-4 sm:px-6 lg:px-8">
              <Providers>
                {children}
                <ToastContainer {...toastConfig} />
              </Providers>
            </div>
          </div>
        </div>
      </body>
    </html>
  );
};

export default RootLayout;
