import classNames, { Argument as ClassNamesArgument } from "classnames";
import { twMerge } from "tailwind-merge";

const classMerge = (...classnames: ClassNamesArgument[]) =>
  twMerge(classNames(classnames));

export default classMerge;
